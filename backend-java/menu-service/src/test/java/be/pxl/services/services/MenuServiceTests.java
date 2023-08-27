package be.pxl.services.services;

import be.pxl.services.client.PopUpBarClient;
import be.pxl.services.domain.Category;
import be.pxl.services.domain.Cocktail;
import be.pxl.services.domain.Menu;
import be.pxl.services.domain.MenuItem;
import be.pxl.services.domain.dto.CocktailResponse;
import be.pxl.services.domain.dto.MenuRequest;
import be.pxl.services.domain.dto.MenuResponse;
import be.pxl.services.repository.CocktailRepository;
import be.pxl.services.repository.MenuRepository;
import be.pxl.services.services.CocktailService;
import be.pxl.services.services.MenuService;
import org.junit.jupiter.api.Test;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.context.SpringBootTest;
import org.testcontainers.junit.jupiter.Testcontainers;
import java.util.*;
import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertFalse;
import static org.mockito.Mockito.verify;
import static org.mockito.Mockito.when;

@SpringBootTest
@Testcontainers
@AutoConfigureMockMvc
public class MenuServiceTests {

    @Mock
    private MenuRepository menuRepository;
    @Mock
    private CocktailRepository cocktailRepository;

    @InjectMocks
    private MenuService menuService;

    @Mock
    private CocktailService cocktailService;

    @Autowired
    private PopUpBarClient popUpBarClient;

    @Test
    public void testGetAllMenus() {
        // Arrange
        List<MenuItem> cocktails = new ArrayList<MenuItem>();
        List<Cocktail> orderCocktails = new ArrayList<Cocktail>();
        Menu menu1 = Menu.builder()
                .id(1L)
                .popUpBarId(1L)
                .cocktails(cocktails)
                .orderCocktails(orderCocktails)
                .build();

        Menu menu2 = Menu.builder()
                .id(2L)
                .popUpBarId(2L)
                .cocktails(cocktails)
                .orderCocktails(orderCocktails)
                .build();
        List<Menu> menus = Arrays.asList(menu1, menu2);
        when(menuRepository.findAll()).thenReturn(menus);

        // Act
        List<MenuResponse> result = menuService.getAllMenus();

        // Assert
        assertEquals(2, result.size());
        assertEquals(1, result.get(0).getId());
        assertEquals(1, result.get(0).getPopUpBarId());
        assertEquals(0, result.get(0).getCocktails().size());
        assertEquals(0, result.get(0).getOrderCocktails().size());
        assertEquals(2, result.get(1).getId());
        assertEquals(2, result.get(1).getPopUpBarId());
        assertEquals(0, result.get(1).getCocktails().size());
        assertEquals(0, result.get(1).getOrderCocktails().size());
    }

    @Test
    public void testGetMenuById() {
        // Arrange
        List<MenuItem> cocktails = new ArrayList<MenuItem>();
        List<Cocktail> orderCocktails = new ArrayList<Cocktail>();
        Long id = 1L;
        Menu menu1 = Menu.builder()
                .id(id)
                .popUpBarId(1L)
                .cocktails(cocktails)
                .orderCocktails(orderCocktails)
                .build();
        when(menuRepository.findById(id)).thenReturn(Optional.of(menu1));


        //Act
        MenuResponse response = menuService.getMenuById(id);

        //Assert
        assertEquals(id, response.getId());
        assertEquals(1L, response.getPopUpBarId());
        assertEquals(0, response.getCocktails().size());
        assertEquals(0, response.getOrderCocktails().size());
    }

    @Test
    public void testChangeMenu() {
        // Arrange
        List<MenuItem> cocktails = new ArrayList<MenuItem>();
        List<Cocktail> orderCocktails = new ArrayList<Cocktail>();
        Long id = 1L;
        MenuRequest menuRequest = MenuRequest.builder()
                .id(id)
                .cocktails(cocktails)
                .popUpBarId(1L)
                .orderCocktails(orderCocktails)
                .build();

        Menu menu = Menu.builder()
                .id(id)
                .popUpBarId(1L)
                .cocktails(cocktails)
                .orderCocktails(orderCocktails)
                .build();
        // Act
        when(menuRepository.findById(id)).thenReturn(Optional.of(menu));
        when(menuRepository.save(menu)).thenReturn(menu);

        menuService.changeMenu(menuRequest, id);
        MenuResponse response = menuService.getMenuById(id);

        // Assert
        assertEquals(id, response.getId());
        assertEquals(1L, response.getPopUpBarId());
        assertEquals(0, response.getCocktails().size());
        assertEquals(0, response.getOrderCocktails().size());
    }

    @Test
    public void testDeleteCocktail() {
        // Arrange
        List<MenuItem> cocktails = new ArrayList<MenuItem>();
        List<Cocktail> orderCocktails = new ArrayList<Cocktail>();
        Long id = 1L;
        Menu menu = Menu.builder()
                .id(id)
                .popUpBarId(1L)
                .cocktails(cocktails)
                .orderCocktails(orderCocktails)
                .build();
        menuRepository.save(menu);


        menuService.deleteMenu(id);


        Optional<Menu> deletedMenu = menuRepository.findById(id);
        assertFalse(deletedMenu.isPresent());
    }

    @Test
    public void testGetMenuByPopupbarId() {
        // Arrange
        List<MenuItem> cocktails = new ArrayList<MenuItem>();
        List<Cocktail> orderCocktails = new ArrayList<Cocktail>();
        Long id = 1L;
        Menu menu1 = Menu.builder()
                .id(1L)
                .popUpBarId(id)
                .cocktails(cocktails)
                .orderCocktails(orderCocktails)
                .build();
        when(menuRepository.findMenuByPopUpBarId(id)).thenReturn(menu1);


        //Act
        MenuResponse response = menuService.getMenuByPopupbarId(id);

        //Assert
        assertEquals(1L, response.getId());
        assertEquals(id, response.getPopUpBarId());
        assertEquals(0, response.getCocktails().size());
        assertEquals(0, response.getOrderCocktails().size());
    }

    @Test
    public void testGetMenuByCocktailId() {
        // Arrange
        Cocktail cocktail = Cocktail.builder()
                .serialNumber(4070071967075L)
                .name("Margarita")
                .purchasePrice(6.00)
                .category(Category.AllDay)
                .imageUrl("https://www.example.com/margarita.jpg")
                .build();
        List<MenuItem> cocktails = new ArrayList<MenuItem>();
        MenuItem menuItem = MenuItem.builder()
                .cocktail(cocktail)
                .sellingPrice(2.0)
                .build();
        cocktails.add(menuItem);
        List<Cocktail> orderCocktails = new ArrayList<Cocktail>();
        Long id = 1L;
        Menu menu1 = Menu.builder()
                .id(1L)
                .popUpBarId(id)
                .cocktails(cocktails)
                .orderCocktails(orderCocktails)
                .build();
        List<Menu> menus = new ArrayList<>();
        menus.add(menu1);
        when(menuRepository.findAll()).thenReturn(menus);


        //Act
        MenuResponse response = menuService.getMenuByCocktailId(4070071967075L);

        //Assert
        assertEquals(1L, response.getId());
        assertEquals(id, response.getPopUpBarId());
        assertEquals(1, response.getCocktails().size());
        assertEquals(0, response.getOrderCocktails().size());
    }

    @Test
    public void testAddCocktailToMenu() {
        // Arrange
        Cocktail cocktail = Cocktail.builder()
                .serialNumber(4070071967075L)
                .name("Margarita")
                .purchasePrice(6.00)
                .category(Category.AllDay)
                .imageUrl("https://www.example.com/margarita.jpg")
                .build();
        List<MenuItem> cocktails = new ArrayList<MenuItem>();
        List<Cocktail> orderCocktails = new ArrayList<Cocktail>();
        Long id = 1L;
        UUID uuid = UUID.randomUUID();
        Menu menu1 = Menu.builder()
                .uuid(uuid)
                .id(1L)
                .popUpBarId(id)
                .cocktails(cocktails)
                .orderCocktails(orderCocktails)
                .build();
        when(menuRepository.save(menu1)).thenReturn(menu1);
        when(menuRepository.findById(id)).thenReturn(Optional.of(menu1));
        when(cocktailRepository.findById(4070071967075L)).thenReturn(Optional.of(cocktail));
        menuService.addCocktailToMenu(id,4070071967075L,2.0);


        //Act
        MenuResponse response = menuService.getMenuById(id);

        //Assert
        assertEquals(1L, response.getId());
        assertEquals(id, response.getPopUpBarId());
        assertEquals(1, response.getCocktails().size());
        assertEquals(0, response.getOrderCocktails().size());
    }

    @Test
    public void testGetOrderCocktailsByPopupbarId() throws Exception {
        // Arrange
        List<MenuItem> cocktails = new ArrayList<MenuItem>();
        List<Cocktail> orderCocktails = new ArrayList<Cocktail>();
        Cocktail cocktail = Cocktail.builder()
                .serialNumber(4070071967075L)
                .name("Margarita")
                .purchasePrice(6.00)
                .category(Category.AllDay)
                .imageUrl("https://www.example.com/margarita.jpg")
                .build();
        CocktailResponse cocktailResponse = CocktailResponse.builder()
                .serialNumber(4070071967075L)
                .name("Margarita")
                .purchasePrice(6.00)
                .category(Category.AllDay)
                .imageUrl("https://www.example.com/margarita.jpg")
                .build();
        orderCocktails.add(cocktail);
        Long id = 1L;
        Menu menu1 = Menu.builder()
                .id(1L)
                .popUpBarId(id)
                .cocktails(cocktails)
                .orderCocktails(orderCocktails)
                .build();
        List<Menu> menus = new ArrayList<>();
        menus.add(menu1);
        when(menuRepository.findAll()).thenReturn(menus);
        when(cocktailService.mapToCocktailResponse(cocktail)).thenReturn(cocktailResponse);

        //Act
        List<CocktailResponse> response = menuService.getOrderCocktailsByPopUpBarId(id);

        //Assert
        assertEquals(4070071967075L, response.get(0).getSerialNumber());
        assertEquals("Margarita", response.get(0).getName());
        assertEquals(6.0, response.get(0).getPurchasePrice());
        assertEquals(Category.AllDay, response.get(0).getCategory());
        assertEquals("https://www.example.com/margarita.jpg", response.get(0).getImageUrl());
    }

    @Test
    public void testAddOrderCocktailToPopupbar() throws Exception {
        // Arrange
        Cocktail cocktail = Cocktail.builder()
                .serialNumber(4070071967075L)
                .name("Margarita")
                .purchasePrice(6.00)
                .category(Category.AllDay)
                .imageUrl("https://www.example.com/margarita.jpg")
                .build();
        List<MenuItem> cocktails = new ArrayList<MenuItem>();
        List<Cocktail> orderCocktails = new ArrayList<Cocktail>();
        Long id = 1L;
        Menu menu1 = Menu.builder()
                .id(1L)
                .popUpBarId(id)
                .cocktails(cocktails)
                .orderCocktails(orderCocktails)
                .build();
        List<Menu> menus = new ArrayList<>();
        menus.add(menu1);
        when(menuRepository.findAll()).thenReturn(menus);
        when(cocktailRepository.findById(4070071967075L)).thenReturn(Optional.of(cocktail));
        when(menuRepository.findById(id)).thenReturn(Optional.of(menu1));
        menuService.addOrderCocktailToPopUpBar(id,4070071967075L);


        //Act
        MenuResponse response = menuService.getMenuById(id);

        //Assert
        assertEquals(1L, response.getId());
        assertEquals(id, response.getPopUpBarId());
        assertEquals(0, response.getCocktails().size());
        assertEquals(1, response.getOrderCocktails().size());
    }

    @Test
    public void testRemoveCocktailFromMenu() throws Exception {
        // Arrange
        Long id = 1L;
        Long cocktailId = 4070071967075L;
        List<MenuItem> cocktails = new ArrayList<MenuItem>();
        List<Cocktail> orderCocktails = new ArrayList<Cocktail>();
        Cocktail cocktail = Cocktail.builder()
                .serialNumber(cocktailId)
                .name("Margarita")
                .purchasePrice(6.00)
                .category(Category.AllDay)
                .imageUrl("https://www.example.com/margarita.jpg")
                .build();
        MenuItem menuItem = MenuItem.builder()
                .cocktail(cocktail)
                .sellingPrice(2.0)
                .build();
        cocktails.add(menuItem);
        Menu menu1 = Menu.builder()
                .id(id)
                .popUpBarId(1L)
                .cocktails(cocktails)
                .orderCocktails(orderCocktails)
                .build();
        when(cocktailRepository.findById(cocktailId)).thenReturn(Optional.of(cocktail));
        when(menuRepository.findById(id)).thenReturn(Optional.of(menu1));



        //Act
        menuService.removeCocktailFromMenu(id, cocktailId);
        MenuResponse response = menuService.getMenuById(id);


        //Assert
        assertEquals(id, response.getId());
        assertEquals(1L, response.getPopUpBarId());
        assertEquals(0,menu1.getCocktails().size());
    }

    @Test
    public void testChangeCocktailPrice() throws Exception {
        // Arrange
        Long id = 1L;
        Long cocktailId = 4070071967075L;
        List<MenuItem> cocktails = new ArrayList<MenuItem>();
        List<Cocktail> orderCocktails = new ArrayList<Cocktail>();
        Cocktail cocktail = Cocktail.builder()
                .serialNumber(cocktailId)
                .name("Margarita")
                .purchasePrice(6.00)
                .category(Category.AllDay)
                .imageUrl("https://www.example.com/margarita.jpg")
                .build();
        MenuItem menuItem = MenuItem.builder()
                .cocktail(cocktail)
                .sellingPrice(2.0)
                .build();
        cocktails.add(menuItem);
        Menu menu1 = Menu.builder()
                .id(id)
                .popUpBarId(1L)
                .cocktails(cocktails)
                .orderCocktails(orderCocktails)
                .build();
        when(cocktailRepository.findById(cocktailId)).thenReturn(Optional.of(cocktail));
        when(menuRepository.findById(id)).thenReturn(Optional.of(menu1));



        //Act
        menuService.changeCocktailPrice(4.0, cocktailId, id);
        MenuResponse response = menuService.getMenuById(id);


        //Assert
        assertEquals(id, response.getId());
        assertEquals(1L, response.getPopUpBarId());
        assertEquals(1,menu1.getCocktails().size());
        assertEquals(4.0,menu1.getCocktails().get(0).getSellingPrice());
    }
}
