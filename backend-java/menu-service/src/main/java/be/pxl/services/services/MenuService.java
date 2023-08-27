package be.pxl.services.services;

import be.pxl.services.client.PopUpBarClient;
import be.pxl.services.domain.Cocktail;
import be.pxl.services.domain.Menu;
import be.pxl.services.domain.MenuItem;
import be.pxl.services.domain.client.PopUpBarResponse;
import be.pxl.services.domain.dto.CocktailResponse;
import be.pxl.services.domain.dto.MenuRequest;
import be.pxl.services.domain.dto.MenuResponse;
import be.pxl.services.repository.CocktailRepository;
import be.pxl.services.repository.MenuRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Service;
import org.springframework.web.client.RestTemplate;

import java.util.*;

@Service
@RequiredArgsConstructor
public class MenuService implements IMenuService{

    @Autowired
    PopUpBarClient popUpBarClient;

    private final MenuRepository menuRepository;
    private final CocktailRepository cocktailRepository;
    private final CocktailService cocktailService;

    @Override
    public List<MenuResponse> getAllMenus() {
        List<Menu> menus = menuRepository.findAll();
        return menus.stream().map(menu -> mapToMenuResponse(menu)).toList();
    }

    private MenuResponse mapToMenuResponse(Menu menu) {
        return MenuResponse.builder()
                .id(menu.getId())
                .popUpBarId(menu.getPopUpBarId())
                .cocktails(menu.getCocktails())
                .orderCocktails(menu.getOrderCocktails())
                .build();
    }

    @Override
    public void addMenu(MenuRequest menuRequest) {
        UUID uuid = UUID.randomUUID();
        Menu menu = Menu.builder()
                .uuid(uuid)
                .popUpBarId(menuRequest.getPopUpBarId())
                .cocktails(menuRequest.getCocktails())
                .orderCocktails(menuRequest.getOrderCocktails())
                .build();
        Menu newMenu = menuRepository.save(menu);

        ResponseEntity<PopUpBarResponse> popupbarRequest = popUpBarClient.getPopUpBarById(menuRequest.getPopUpBarId());
        PopUpBarResponse popupbar = popupbarRequest.getBody();

        //Send to eventbus
        //Create a map containing the request body data
        Map<String, Object> requestBody = new HashMap<>();

        requestBody.put("menuId", newMenu.getUuid().toString());
        requestBody.put("barName", popupbar.getName());

        //Create a RestTemplate instance
        RestTemplate restTemplate = new RestTemplate();

        //Make a POST request to the specified URL, sending the request body data
        restTemplate.postForObject("http://localhost:8086/api/messaging/addMenu", requestBody, String.class);
    }

    @Override
    public MenuResponse getMenuById(Long id) {
        Menu menu = menuRepository.findById(id).get();
        return mapToMenuResponse(menu);
    }

    @Override
    public MenuResponse getMenuByPopupbarId(Long popupbarId) {
        Menu menu = menuRepository.findMenuByPopUpBarId(popupbarId);
        return mapToMenuResponse(menu);
    }

    @Override
    public void changeMenu(MenuRequest menuRequest, Long id) {
        Menu changedMenu = menuRepository.findById(id)
                .map(menu -> {
                    menu.setPopUpBarId(menuRequest.getPopUpBarId());
                    menu.setCocktails(menuRequest.getCocktails());
                    menu.setOrderCocktails(menuRequest.getOrderCocktails());
                    return  menuRepository.save(menu);
                }).get();

        ResponseEntity<PopUpBarResponse> popupbarRequest = popUpBarClient.getPopUpBarById(menuRequest.getPopUpBarId());
        PopUpBarResponse popupbar = popupbarRequest.getBody();

        //Send to eventbus
        //Create a map containing the request body data
        Map<String, Object> requestBody = new HashMap<>();

        requestBody.put("menuId", changedMenu.getUuid().toString());
        requestBody.put("barName", popupbar.getName());

        //Create a RestTemplate instance
        RestTemplate restTemplate = new RestTemplate();

        //Make a POST request to the specified URL, sending the request body data
        restTemplate.postForObject("http://localhost:8086/api/messaging/addMenu", requestBody, String.class);
    }

    @Override
    public void deleteMenu(Long id) {
        menuRepository.deleteById(id);

        //Send to eventbus
        //Create a map containing the request body data
        Map<String, Object> requestBody = new HashMap<>();

        requestBody.put("menuId", id);

        //Create a RestTemplate instance
        RestTemplate restTemplate = new RestTemplate();

        //Make a POST request to the specified URL, sending the request body data
        restTemplate.postForObject("http://localhost:8086/api/messaging/deleteMenu", requestBody, String.class);
    }

    @Override
    public MenuResponse getMenuByCocktailId(Long id) {
        List<Menu> menus = menuRepository.findAll();
        MenuResponse menuResponse = null;
        for (Menu menu : menus) {
            List<MenuItem> cocktails = menu.getCocktails();
            for(MenuItem item : cocktails){
                if(Objects.equals(item.getCocktail().getSerialNumber(), id)){
                    menuResponse = mapToMenuResponse(menu);
                    break;
                }
            }
        }
        return menuResponse;
    }

    @Override
    public void addCocktailToMenu(Long menuId, Long cocktailId, double price) { //This
        Menu menu = menuRepository.findById(menuId).get();
        Cocktail cocktail = cocktailRepository.findById(cocktailId).get();
        List<MenuItem> cocktails = menu.getCocktails();
        MenuItem menuItem = MenuItem.builder()
                .cocktail(cocktail)
                .sellingPrice(price)
                .build();
        cocktails.add(menuItem);
        menu.setCocktails(cocktails);
        Menu newMenu = menuRepository.save(menu);

        //send to eventbus

        // create a map containing the request body data
        Map<String, Object> requestBody = new HashMap<>();

        requestBody.put("menuId", newMenu.getUuid().toString());
        requestBody.put("cocktailId", cocktailId.toString());
        requestBody.put("price", String.valueOf(price));


        // create a RestTemplate instance
        RestTemplate restTemplate = new RestTemplate();

        // make a POST request to the specified URL, sending the request body data
        restTemplate.postForObject("http://localhost:8086/api/messaging/addCocktailToMenu", requestBody, String.class);

    }

    @Override
    public List<CocktailResponse> getOrderCocktailsByPopUpBarId(Long id) throws Exception {
        List<Menu> menus = menuRepository.findAll();
        List<Cocktail> cocktails = null;
        for (Menu menu : menus) {
            if(menu.getPopUpBarId().equals(id)){
                cocktails = menu.getOrderCocktails();
                break;
            }
        }
        if(cocktails == null){
            throw new Exception("no popupbar found");
        }else if(cocktails.isEmpty()){
            throw new Exception("there are no cocktails");
        }else{
            return cocktails.stream().map(cocktail -> cocktailService.mapToCocktailResponse(cocktail)).toList();
        }
    }

    @Override
    public void addOrderCocktailToPopUpBar(Long idPopUpBar, Long idCocktail) throws Exception {
        Long id = null;
        List<Menu> menus = menuRepository.findAll();
        Cocktail cocktail = cocktailRepository.findById(idCocktail).get();
        List<Cocktail> cocktails = null;
        for (Menu menu : menus) {
            if(menu.getPopUpBarId().equals(idPopUpBar)){
                id = menu.getId();
                cocktails = menu.getOrderCocktails();
                cocktails.add(cocktail);
                menu.setOrderCocktails(cocktails);
                menuRepository.save(menu);
            }
        }
        if(cocktails == null){
            throw new Exception("no popupbar found");
        }
    }

    @Override
    public void removeCocktailFromMenu(Long id, Long cocktailId) {
        Menu menu = menuRepository.findById(id).get();
        Cocktail cocktail = cocktailRepository.findById(cocktailId).get();
        List<MenuItem> cocktails = menu.getCocktails();
        cocktails.removeIf(item -> item.getCocktail().equals(cocktail));
        menuRepository.findById(id)
                .map(changeMenu -> {
                    changeMenu.setCocktails(cocktails);
                    return  menuRepository.save(changeMenu);
                });
        //Send to eventbus
        //Create a map containing the request body data
        Map<String, Object> requestBody = new HashMap<>();

        requestBody.put("menuId", id);
        requestBody.put("serialNumber", cocktailId);

        //Create a RestTemplate instance
        RestTemplate restTemplate = new RestTemplate();

        //Make a POST request to the specified URL, sending the request body data
        restTemplate.postForObject("http://localhost:8086/api/messaging/deleteCocktailFromMenu", requestBody, String.class);

    }

    @Override
    public void changeCocktailPrice(double cocktailPrice, Long cocktailId, Long menuId) {
        Menu menu = menuRepository.findById(menuId).get();
        Cocktail cocktail = cocktailRepository.findById(cocktailId).get();
        List<MenuItem> cocktails = menu.getCocktails();
        for (MenuItem item: cocktails)
        {
            if(item.getCocktail().equals(cocktail)){
                item.setSellingPrice(cocktailPrice);
            }
        }
        menuRepository.findById(menuId)
                .map(changeMenu -> {
                    changeMenu.setCocktails(cocktails);
                    return  menuRepository.save(changeMenu);
                });
    }
}
