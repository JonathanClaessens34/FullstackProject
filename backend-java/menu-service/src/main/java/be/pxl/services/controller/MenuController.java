package be.pxl.services.controller;

import be.pxl.services.domain.dto.MenuRequest;
import be.pxl.services.services.IMenuService;
import lombok.RequiredArgsConstructor;
import org.json.simple.JSONObject;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.annotation.Secured;
import org.springframework.web.bind.annotation.*;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServletRequest;


@RestController
@RequestMapping("/api")
@RequiredArgsConstructor
public class MenuController {

    private final IMenuService menuService;

    @GetMapping("/allMenus")
    public ResponseEntity getMenu(){
        return new ResponseEntity(menuService.getAllMenus(), HttpStatus.OK);
    }

    @PostMapping("/createMenu")
    @ResponseStatus(HttpStatus.CREATED)
    public void addMenu(@RequestBody MenuRequest menuRequest){
        menuService.addMenu(menuRequest);
    }

    @GetMapping("/menu/{id}")
    public ResponseEntity getMenuById(@PathVariable Long id){
        return new ResponseEntity(menuService.getMenuById(id), HttpStatus.OK);
    }

    @GetMapping("/menu/popupbar/{id}")
    public ResponseEntity getMenuByPopupbarId(@PathVariable Long id) {
        return new ResponseEntity(menuService.getMenuByPopupbarId(id), HttpStatus.OK);
    }

    @GetMapping("/menu/cocktail/{id}")
    public ResponseEntity getMenuByCocktailId(@PathVariable Long id){
        return new ResponseEntity(menuService.getMenuByCocktailId(id), HttpStatus.OK);
    }

    @PutMapping("/menu/{id}")
    @ResponseStatus(HttpStatus.OK)
    public void changeMenu(@RequestBody MenuRequest menuRequest, @PathVariable Long id){
        menuService.changeMenu(menuRequest, id);
    }

    @DeleteMapping("/menu/{id}")
    @ResponseStatus(HttpStatus.OK)
    public void deleteMenu(@PathVariable Long id){
        menuService.deleteMenu(id);
    }

    @PutMapping("/addCocktailToMenu/{id}")
    @ResponseStatus(HttpStatus.OK)
    public ResponseEntity addCocktailToMenu(@RequestBody JSONObject jsonRequest, @PathVariable Long id){ //Use this
        Long cocktailId = ((Number)jsonRequest.get("serialNumber")).longValue();
        double price = ((Number)jsonRequest.get("price")).doubleValue();
        menuService.addCocktailToMenu(id, cocktailId, price);
        return new ResponseEntity(menuService.getMenuById(id), HttpStatus.OK);
    }

    @PutMapping("/removeCocktailFromMenu/{id}")
    @ResponseStatus(HttpStatus.OK)
    public void removeCocktailFromMenu(@RequestBody JSONObject jsonRequest, @PathVariable Long id){
        Long cocktailId = ((Number)jsonRequest.get("serialNumber")).longValue();
        menuService.removeCocktailFromMenu(id, cocktailId);
    }

    @GetMapping("/menu/order/{id}")
    public ResponseEntity getCocktailOrderByPopUpBarId(@PathVariable Long id) throws Exception {
        return new ResponseEntity(menuService.getOrderCocktailsByPopUpBarId(id), HttpStatus.OK);
    }

    @PutMapping("/addOrderCocktailToBar/{id}")
    @ResponseStatus(HttpStatus.OK)
    public void addOrderCocktailToMenu(@RequestBody JSONObject jsonRequest, @PathVariable Long id) throws Exception{
        Long cocktailId = ((Number)jsonRequest.get("serialNumber")).longValue();
        menuService.addOrderCocktailToPopUpBar(id, cocktailId);
    }

    @PutMapping("/cocktailChangePrice/{id}")
    @ResponseStatus(HttpStatus.OK)
    public void changeCocktailPrice(@RequestBody JSONObject jsonRequest, @PathVariable Long id){
        double cocktailPrice = ((Number)jsonRequest.get("price")).doubleValue();
        Long menuId = ((Number)jsonRequest.get("menuId")).longValue();
        menuService.changeCocktailPrice(cocktailPrice, id, menuId);
    }

    @GetMapping("/exit")
    @ResponseStatus(HttpStatus.OK)
    public void logout(HttpServletRequest request){
        try{
            request.logout();
        }catch (ServletException e){
            throw new RuntimeException(e);
        }
    }
}
