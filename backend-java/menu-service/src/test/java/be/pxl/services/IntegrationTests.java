package be.pxl.services;
import be.pxl.services.client.PopUpBarClient;
import be.pxl.services.controller.MenuController;
import be.pxl.services.domain.Category;
import be.pxl.services.domain.Cocktail;
import be.pxl.services.domain.Menu;
import be.pxl.services.domain.MenuItem;
import be.pxl.services.domain.dto.CocktailRequest;
import be.pxl.services.domain.dto.CocktailResponse;
import be.pxl.services.domain.dto.MenuRequest;
import be.pxl.services.domain.dto.MenuResponse;
import be.pxl.services.repository.CocktailRepository;
import be.pxl.services.repository.MenuRepository;
import be.pxl.services.services.CocktailService;
import be.pxl.services.services.MenuService;
import org.junit.jupiter.api.AfterAll;
import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.Test;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.http.HttpHeaders;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.test.web.servlet.MockMvc;
import org.springframework.test.web.servlet.ResultActions;
import org.springframework.test.web.servlet.request.MockMvcRequestBuilders;
import org.springframework.test.web.servlet.result.MockMvcResultMatchers;
import org.springframework.test.web.servlet.setup.MockMvcBuilders;
import org.springframework.util.Base64Utils;
import org.testcontainers.containers.MySQLContainer;
import org.testcontainers.junit.jupiter.Container;
import org.testcontainers.junit.jupiter.Testcontainers;
import com.fasterxml.jackson.databind.ObjectMapper;
import javax.transaction.Transactional;
import java.util.ArrayList;
import java.util.List;

import static org.hamcrest.MatcherAssert.assertThat;
import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.mockito.Mockito.when;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.post;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.content;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;


@Testcontainers
@SpringBootTest
@AutoConfigureMockMvc
public class IntegrationTests {

    @Autowired
    private MockMvc mockMvc;

    @MockBean
    private MenuService menuService;

    @Autowired
    private MenuRepository menuRepository;

    @Autowired
    private CocktailRepository cocktailRepository;

    @MockBean
    private CocktailService cocktailService;

    @Container
    private static MySQLContainer mysqlContainer = new MySQLContainer();



    @BeforeAll
    static void beforeAll() {
        mysqlContainer.start();
    }

    @AfterAll
    static void afterAll() {
        mysqlContainer.stop();
    }

    @Test
    @Transactional
    void testAddMenu() throws Exception {
        // Set up the mock request
        List<MenuItem> cocktails = new ArrayList<>();
        List<Cocktail> orderCocktails = new ArrayList<>();
        MenuRequest menuRequest = MenuRequest.builder()
                .id(1L)
                .popUpBarId(2L)
                .cocktails(cocktails)
                .orderCocktails(orderCocktails)
                .build();
        MenuResponse menu = MenuResponse.builder()
                .id(1L)
                .popUpBarId(2L)
                .cocktails(cocktails)
                .orderCocktails(orderCocktails)
                .build();
        when(menuService.getMenuById(1L)).thenReturn(menu);
        when(menuService.getMenuByPopupbarId(2L)).thenReturn(menu);

        // Perform the post request menu
        mockMvc.perform(post("/api/createMenu")
                        .header(HttpHeaders.AUTHORIZATION,
                                "Basic " + Base64Utils.encodeToString("brewer:brewer123".getBytes()))
                        .contentType(MediaType.APPLICATION_JSON)
                        .content(new ObjectMapper().writeValueAsString(menuRequest)))
                .andExpect(status().isCreated());

        // Verify that the menu was added to the repository
        MenuResponse addedMenu = menuService.getMenuByPopupbarId(menuRequest.getPopUpBarId());
        assertEquals(menuRequest.getCocktails(), addedMenu.getCocktails());
        assertEquals(menuRequest.getOrderCocktails(), addedMenu.getOrderCocktails());

        // Perform the get by id menu
        mockMvc.perform(get("/api/menu/{id}", 1L)
                        .header(HttpHeaders.AUTHORIZATION,
                                "Basic " + Base64Utils.encodeToString("brewer:brewer123".getBytes()))
                        ).andExpect(status().isOk())
                        .andExpect(content().json("{id: 1}"));

    }

    @Test
    @Transactional
    void testAddCocktail() throws Exception {
        // Set up the mock request
        CocktailRequest cocktailRequest = CocktailRequest.builder()
                .serialNumber(4070071967075L)
                .name("Mojito")
                .purchasePrice(8.50)
                .category(Category.AllDay)
                .imageUrl("http://localhost:8080/images/mojito.jpg")
                .build();
        CocktailResponse cocktail = CocktailResponse.builder()
                .serialNumber(4070071967075L)
                .name("Mojito")
                .purchasePrice(8.50)
                .category(Category.AllDay)
                .imageUrl("http://localhost:8080/images/mojito.jpg")
                .build();
        when(cocktailService.getCocktailById(4070071967075L)).thenReturn(cocktail);

        // Perform the post request cocktail
        mockMvc.perform(post("/api/createCocktail")
                        .header(HttpHeaders.AUTHORIZATION,
                                "Basic " + Base64Utils.encodeToString("brewer:brewer123".getBytes()))
                        .contentType(MediaType.APPLICATION_JSON)
                        .content(new ObjectMapper().writeValueAsString(cocktailRequest)))
                .andExpect(status().isCreated());

        // Verify that the cocktail was added to the repository
        CocktailResponse addedCocktail = cocktailService.getCocktailById(cocktailRequest.getSerialNumber());
        assertEquals(cocktailRequest.getSerialNumber(), addedCocktail.getSerialNumber());
        assertEquals(cocktailRequest.getName(), addedCocktail.getName());

        // Perform the get by id menu
        mockMvc.perform(get("/api/cocktail/{id}", 4070071967075L)
                .header(HttpHeaders.AUTHORIZATION,
                        "Basic " + Base64Utils.encodeToString("brewer:brewer123".getBytes()))
        )
                .andExpect(status().isOk());
    }

}

