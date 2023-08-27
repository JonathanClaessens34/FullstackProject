package be.pxl.services.repository;

import be.pxl.services.domain.Cocktail;
import be.pxl.services.domain.Menu;
import be.pxl.services.domain.MenuItem;
import be.pxl.services.domain.client.PopUpBar;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.data.jdbc.DataJdbcTest;
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
public class MenuRepositoryTests {
    @Autowired
    private MenuRepository menuRepository;

    /*@Test
    public void testSaveMenu() {
        menuRepository.deleteAll();
        Menu menu = Menu.builder()
                .popUpBarId(1L)
                .cocktails(new ArrayList<>())
                .orderCocktails(new ArrayList<>())
                .build();

        menuRepository.save(menu);

        List<Menu> menus = menuRepository.findAll();
        assertNotNull(menus);
        assertEquals(1, menus.size());
        assertEquals(1L, menus.get(0).getPopUpBarId());
        assertTrue(menus.get(0).getCocktails().isEmpty());
        assertTrue(menus.get(0).getOrderCocktails().isEmpty());
    }*/

    @Test
    public void testDeletePopUpBar() {
        Menu menu = Menu.builder()
                .popUpBarId(1L)
                .cocktails(new ArrayList<>())
                .orderCocktails(new ArrayList<>())
                .build();
        menuRepository.save(menu);

        menuRepository.delete(menu);

        Optional<Menu> deletedPopUpBar = menuRepository.findById(menu.getId());
        assertFalse(deletedPopUpBar.isPresent());
    }

}
