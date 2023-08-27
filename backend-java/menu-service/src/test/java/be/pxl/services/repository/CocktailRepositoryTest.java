package be.pxl.services.repository;

import be.pxl.services.domain.Category;
import be.pxl.services.domain.Cocktail;
import be.pxl.services.domain.Menu;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.jdbc.AutoConfigureTestDatabase;
import org.springframework.boot.test.autoconfigure.orm.jpa.DataJpaTest;
import org.springframework.test.context.junit4.SpringRunner;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

import static org.junit.jupiter.api.Assertions.*;

@RunWith(SpringRunner.class)
@DataJpaTest
@AutoConfigureTestDatabase(replace = AutoConfigureTestDatabase.Replace.NONE)
public class CocktailRepositoryTest {
    @Autowired
    private CocktailRepository cocktailRepository;

    @Test
    public void testSaveMenu() {
        cocktailRepository.deleteAll();
        Cocktail cocktail = Cocktail.builder()
                .serialNumber(4070071967075L)
                .name("Mojito")
                .purchasePrice(8.50)
                .category(Category.AllDay)
                .imageUrl("http://localhost:8080/images/mojito.jpg")
                .build();

        cocktailRepository.save(cocktail);

        List<Cocktail> cocktails = cocktailRepository.findAll();
        assertNotNull(cocktails);
        assertEquals(1, cocktails.size());
        assertEquals(4070071967075L, cocktails.get(0).getSerialNumber());
        assertEquals("Mojito", cocktails.get(0).getName());
        assertEquals(8.50, cocktails.get(0).getPurchasePrice());
        assertEquals(Category.AllDay, cocktails.get(0).getCategory());
        assertEquals("http://localhost:8080/images/mojito.jpg", cocktails.get(0).getImageUrl());
    }

    @Test
    public void testDeletePopUpBar() {
        cocktailRepository.deleteAll();
        Cocktail cocktail = Cocktail.builder()
                .serialNumber(4070071967075L)
                .name("Mojito")
                .purchasePrice(8.50)
                .category(Category.AllDay)
                .imageUrl("http://localhost:8080/images/mojito.jpg")
                .build();
        cocktailRepository.save(cocktail);

        cocktailRepository.delete(cocktail);

        Optional<Cocktail> deletedPopUpBar = cocktailRepository.findById(cocktail.getSerialNumber());
        assertFalse(deletedPopUpBar.isPresent());
    }
}