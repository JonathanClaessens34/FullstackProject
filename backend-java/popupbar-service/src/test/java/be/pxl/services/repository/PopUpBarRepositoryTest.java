package be.pxl.services.repository;

import be.pxl.services.domain.PopUpBar;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.jdbc.AutoConfigureTestDatabase;
import org.springframework.boot.test.autoconfigure.orm.jpa.DataJpaTest;
import org.springframework.test.context.junit4.SpringRunner;

import java.util.Optional;

import static org.junit.jupiter.api.Assertions.*;

@RunWith(SpringRunner.class)
@DataJpaTest
@AutoConfigureTestDatabase(replace = AutoConfigureTestDatabase.Replace.NONE)
public class PopUpBarRepositoryTest {
    @Autowired
    private PopUpBarRepository popUpBarRepository;

    @Test
    public void testSavePopUpBar() {
        popUpBarRepository.deleteAll();
        PopUpBar popUpBar = PopUpBar.builder()
                .name("Test Pop-Up Bar")
                .location("Test Location")
                .brewer("Test Brewer")
                .build();
        popUpBarRepository.save(popUpBar);

        PopUpBar savedPopUpBar = popUpBarRepository.findById(popUpBar.getId()).get();
        assertNotNull(savedPopUpBar);
        assertEquals("Test Pop-Up Bar", savedPopUpBar.getName());
        assertEquals("Test Location", savedPopUpBar.getLocation());
        assertEquals("Test Brewer", savedPopUpBar.getBrewer());
    }

    @Test
    public void testDeletePopUpBar() {
        popUpBarRepository.deleteAll();
        PopUpBar popUpBar = PopUpBar.builder()
                .name("Test Pop-Up Bar")
                .location("Test Location")
                .brewer("Test Brewer")
                .build();
        popUpBarRepository.save(popUpBar);

        popUpBarRepository.delete(popUpBar);

        Optional<PopUpBar> deletedPopUpBar = popUpBarRepository.findById(popUpBar.getId());
        assertFalse(deletedPopUpBar.isPresent());
    }

}