package be.pxl.services.services;

import be.pxl.services.client.MenuClient;
import be.pxl.services.domain.PopUpBar;
import be.pxl.services.domain.client.Category;
import be.pxl.services.domain.client.Cocktail;
import be.pxl.services.domain.client.MenuItem;
import be.pxl.services.domain.client.MenuRequest;
import be.pxl.services.domain.dto.PopUpBarRequest;
import be.pxl.services.domain.dto.PopUpBarResponse;
import be.pxl.services.repository.PopUpBarRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;

@Service
@RequiredArgsConstructor
public class PopUpBarService implements IPopUpBarService {
    @Autowired
    MenuClient menuClient;

    private final PopUpBarRepository popUpBarRepository;
    @Override
    public List<PopUpBarResponse> getAllPopUpBars() {
        List<PopUpBar> popUpBars = popUpBarRepository.findAll();
        return popUpBars.stream().map(popUpBar -> mapToMenuResponse(popUpBar)).toList();
    }

    private PopUpBarResponse mapToMenuResponse(PopUpBar popUpBar) {
        return PopUpBarResponse.builder()
                .id(popUpBar.getId())
                .name(popUpBar.getName())
                .location(popUpBar.getLocation())
                .brewer(popUpBar.getBrewer())
                .build();
    }
    private Long popUpBarId;
    private List<MenuItem> cocktails;
    private Category category;
    private String imageLocationUrl;
    private List<Cocktail> orderCocktails;
    @Override
    public void addPopUpBar(PopUpBarRequest popUpBarRequest) {
        PopUpBar popUpBar = PopUpBar.builder()
                .name(popUpBarRequest.getName())
                .location(popUpBarRequest.getLocation())
                .brewer(popUpBarRequest.getBrewer())
                .build();
        popUpBarRepository.save(popUpBar);
        MenuRequest menuRequest = MenuRequest.builder()
                .popUpBarId(popUpBar.getId())
                .cocktails(new ArrayList<>())
                .orderCocktails(new ArrayList<>())
                .build();
        menuClient.addMenu(menuRequest);
    }

    @Override
    public PopUpBarResponse getPopUpBarById(Long id) {
        PopUpBar popUpBar = popUpBarRepository.findById(id).get();
        return mapToMenuResponse(popUpBar);
    }

    @Override
    public void changePopUpBar(PopUpBarRequest popUpBarRequest, Long id) {
        popUpBarRepository.findById(id)
                .map(popUpBar -> {
                   popUpBar.setName(popUpBarRequest.getName());
                   popUpBar.setLocation(popUpBarRequest.getLocation());
                   popUpBar.setBrewer(popUpBarRequest.getBrewer());
                   return popUpBarRepository.save(popUpBar);
                });
    }

    @Override
    public void deletePopUpBar(Long id) {
        popUpBarRepository.deleteById(id);
    }
}
