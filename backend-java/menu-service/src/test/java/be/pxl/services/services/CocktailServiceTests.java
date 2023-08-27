package be.pxl.services.services;

import be.pxl.services.domain.Category;
import be.pxl.services.domain.Cocktail;
import be.pxl.services.domain.dto.CocktailRequest;
import be.pxl.services.domain.dto.CocktailResponse;
import be.pxl.services.repository.CocktailRepository;
import org.junit.jupiter.api.Test;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.context.SpringBootTest;
import java.util.Arrays;
import java.util.List;
import java.util.Optional;
import static org.junit.Assert.assertNotNull;
import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertFalse;
import static org.mockito.Mockito.when;


@SpringBootTest
@AutoConfigureMockMvc
public class CocktailServiceTests {

    @Mock
    private CocktailRepository cocktailRepository;

    @InjectMocks
    private CocktailService cocktailService;

    @Test
    public void testGetAllCocktails() {
        // Arrange
        Cocktail cocktail1 = new Cocktail(1L, "Cocktail 1", 10.5, Category.AllDay, "image1.jpg");
        Cocktail cocktail2 = new Cocktail(2L, "Cocktail 2", 15.0, Category.AfterDinner, "image2.jpg");
        List<Cocktail> cocktails = Arrays.asList(cocktail1, cocktail2);
        when(cocktailRepository.findAll()).thenReturn(cocktails);

        // Act
        List<CocktailResponse> result = cocktailService.getAllCocktails();

        // Assert
        assertEquals(2, result.size());
        assertEquals(1L, result.get(0).getSerialNumber());
        assertEquals("Cocktail 1", result.get(0).getName());
        assertEquals(10.5, result.get(0).getPurchasePrice());
        assertEquals(Category.AllDay, result.get(0).getCategory());
        assertEquals("image1.jpg", result.get(0).getImageUrl());
        assertEquals(2L, result.get(1).getSerialNumber());
        assertEquals("Cocktail 2", result.get(1).getName());
        assertEquals(15.0, result.get(1).getPurchasePrice());
        assertEquals(Category.AfterDinner, result.get(1).getCategory());
        assertEquals("image2.jpg", result.get(1).getImageUrl());
    }

    @Test
    public void testAddCocktail() {
        // Arrange
        CocktailRequest cocktailRequest = CocktailRequest.builder()
                .serialNumber(4070071967075L)
                .name("Mojito")
                .purchasePrice(8.50)
                .category(Category.AllDay)
                .imageUrl("http://localhost:8080/images/mojito.jpg")
                .build();
        Cocktail cocktail = Cocktail.builder()
                .serialNumber(4070071967075L)
                .name("Mojito")
                .purchasePrice(8.50)
                .category(Category.AllDay)
                .imageUrl("http://localhost:8080/images/mojito.jpg")
                .build();
        // Act
        when(cocktailRepository.save(cocktail)).thenReturn(cocktail);
        cocktailService.addCocktail(cocktailRequest);
        when(cocktailRepository.findById(4070071967075L)).thenReturn(Optional.of(cocktail));


        // Assert
        Cocktail savedCocktail = cocktailRepository.findById(4070071967075L).get();
        assertNotNull(savedCocktail);
        assertEquals("Mojito", savedCocktail.getName());
        assertEquals(8.50, savedCocktail.getPurchasePrice(), 0);
        assertEquals(Category.AllDay, savedCocktail.getCategory());
        assertEquals("http://localhost:8080/images/mojito.jpg", savedCocktail.getImageUrl());

    }

    @Test
    public void testGetCocktailById() {
        // Arrange
        Long id = 4070071967075L;
        Cocktail cocktail = Cocktail.builder()
                .serialNumber(id)
                .name("Mojito")
                .purchasePrice(8.50)
                .category(Category.AllDay)
                .imageUrl("http://localhost:8080/images/mojito.jpg")
                .build();
        when(cocktailRepository.findById(id)).thenReturn(Optional.of(cocktail));


        //Act
        CocktailResponse response = cocktailService.getCocktailById(id);

        //Assert
        assertEquals(id, response.getSerialNumber());
        assertEquals("Mojito", response.getName());
        assertEquals(8.50, response.getPurchasePrice(), 0);
        assertEquals(Category.AllDay, response.getCategory());
        assertEquals("http://localhost:8080/images/mojito.jpg", response.getImageUrl());
    }

    @Test
    public void testChangeCocktail() {
        // Arrange
        CocktailRequest cocktailRequest = CocktailRequest.builder()
                .serialNumber(4070071967075L)
                .name("Mojito")
                .purchasePrice(8.50)
                .category(Category.AllDay)
                .imageUrl("https://www.example.com/cosmopolitan.jpg")
                .build();

        Cocktail cocktail = Cocktail.builder()
                .serialNumber(4070071967075L)
                .name("Margarita")
                .purchasePrice(6.00)
                .category(Category.AllDay)
                .imageUrl("https://www.example.com/margarita.jpg")
                .build();
        // Act
        when(cocktailRepository.findById(4070071967075L)).thenReturn(Optional.of(cocktail));
        when(cocktailRepository.save(cocktail)).thenReturn(cocktail);

        cocktailService.changeCocktail(cocktailRequest, 4070071967075L);
        CocktailResponse response = cocktailService.getCocktailById(4070071967075L);

        // Assert
        assertEquals(4070071967075L, response.getSerialNumber());
        assertEquals("Mojito", response.getName());
        assertEquals(8.50, response.getPurchasePrice(), 0);
        assertEquals(Category.AllDay, response.getCategory());
        assertEquals("https://www.example.com/cosmopolitan.jpg", response.getImageUrl());
    }

    @Test
    public void testDeleteCocktail() {

        Cocktail cocktail = Cocktail.builder()
                .serialNumber(4070071967075L)
                .name("Mojito")
                .purchasePrice(8.50)
                .category(Category.AllDay)
                .imageUrl("http://localhost:8080/images/mojito.jpg")
                .build();
        cocktailRepository.save(cocktail);


        cocktailService.deleteCocktail(4070071967075L);


        Optional<Cocktail> deletedCocktail = cocktailRepository.findById(4070071967075L);
        assertFalse(deletedCocktail.isPresent());
    }
}

