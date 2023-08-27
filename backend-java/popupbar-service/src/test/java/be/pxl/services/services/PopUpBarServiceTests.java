package be.pxl.services.services;

import be.pxl.services.client.MenuClient;
import be.pxl.services.domain.PopUpBar;
import be.pxl.services.domain.dto.PopUpBarRequest;
import be.pxl.services.domain.dto.PopUpBarResponse;
import be.pxl.services.repository.PopUpBarRepository;
import org.junit.jupiter.api.Test;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.context.SpringBootTest;
import org.testcontainers.junit.jupiter.Testcontainers;

import java.util.Arrays;
import java.util.List;
import java.util.Optional;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertFalse;
import static org.mockito.Mockito.when;

@SpringBootTest
@Testcontainers
@AutoConfigureMockMvc
public class PopUpBarServiceTests {

    @Mock
    private PopUpBarRepository popUpBarRepository;

    @InjectMocks
    private PopUpBarService popUpBarService;

    @Autowired
    private MenuClient menuClient;

    @Test
    public void testGetAllPopUpBars() {
        // Arrange
        PopUpBar popUpBar1 = PopUpBar.builder()
                .id(1L)
                .brewer("brewer")
                .location("location")
                .name("bar1")
                .build();
        PopUpBar popUpBar2 = PopUpBar.builder()
                .id(2L)
                .brewer("brewer")
                .location("location")
                .name("bar2")
                .build();
        List<PopUpBar> popUpBars = Arrays.asList(popUpBar1, popUpBar2);
        when(popUpBarRepository.findAll()).thenReturn(popUpBars);

        // Act
        List<PopUpBarResponse> result = popUpBarService.getAllPopUpBars();

        // Assert
        assertEquals(2, result.size());
        assertEquals(1L, result.get(0).getId());
        assertEquals("bar1", result.get(0).getName());
        assertEquals("brewer", result.get(0).getBrewer());
        assertEquals("location", result.get(0).getLocation());
        assertEquals(2L, result.get(1).getId());
        assertEquals("bar2", result.get(1).getName());
        assertEquals("brewer", result.get(1).getBrewer());
        assertEquals("location", result.get(1).getLocation());
    }

    @Test
    public void testGetPopUpBarById() {
        // Arrange
        Long id = 1L;
        PopUpBar popUpBar = PopUpBar.builder()
                .id(id)
                .brewer("brewer")
                .location("location")
                .name("bar1")
                .build();
        when(popUpBarRepository.findById(id)).thenReturn(Optional.of(popUpBar));


        //Act
        PopUpBarResponse response = popUpBarService.getPopUpBarById(id);

        //Assert
        assertEquals(id, response.getId());
        assertEquals("bar1", response.getName());
        assertEquals("brewer", response.getBrewer());
        assertEquals("location", response.getLocation());
    }

   /* @Test
    public void testAddPopUpBar() {
        // Arrange
        PopUpBar popUpBar = PopUpBar.builder()
                .id(1L)
                .brewer("brewer")
                .location("location")
                .name("bar1")
                .build();
        PopUpBarRequest popUpBarRequest = PopUpBarRequest.builder()
                .brewer("brewer")
                .location("location")
                .name("bar")
                .build();

        // Act
        when(popUpBarRepository.findById(1L)).thenReturn(Optional.of(popUpBar));
        popUpBarService.addPopUpBar(popUpBarRequest);
        PopUpBarResponse response = popUpBarService.getPopUpBarById(1L);

        // Assert
        assertEquals(1L, response.getId());
        assertEquals("bar2", response.getName());
        assertEquals("brewer2", response.getBrewer());
        assertEquals("location2", response.getLocation());

    }*/

    @Test
    public void testChangePopUpBar() {
        // Arrange
        PopUpBarRequest popUpBarRequest = PopUpBarRequest.builder()
                .brewer("brewer2")
                .location("location2")
                .name("bar2")
                .build();

        PopUpBar popUpBar = PopUpBar.builder()
                .id(1L)
                .brewer("brewer")
                .location("location")
                .name("bar1")
                .build();
        // Act
        when(popUpBarRepository.findById(1L)).thenReturn(Optional.of(popUpBar));
        when(popUpBarRepository.save(popUpBar)).thenReturn(popUpBar);

        popUpBarService.changePopUpBar(popUpBarRequest, 1L);
        PopUpBarResponse response = popUpBarService.getPopUpBarById(1L);

        // Assert
        assertEquals(1L, response.getId());
        assertEquals("bar2", response.getName());
        assertEquals("brewer2", response.getBrewer());
        assertEquals("location2", response.getLocation());

    }

    @Test
    public void testDeletePopUpBar() {

        PopUpBar popUpBar = PopUpBar.builder()
                .id(1L)
                .brewer("brewer")
                .location("location")
                .name("bar1")
                .build();
        popUpBarRepository.save(popUpBar);


        popUpBarService.deletePopUpBar(1L);


        Optional<PopUpBar> deletedPopupbar = popUpBarRepository.findById(1L);
        assertFalse(deletedPopupbar.isPresent());
    }

}
