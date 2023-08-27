package be.pxl.services.services;

import be.pxl.services.domain.Cocktail;
import be.pxl.services.domain.dto.CocktailResponse;
import be.pxl.services.domain.dto.MenuRequest;
import be.pxl.services.domain.dto.MenuResponse;

import java.util.List;

public interface IMenuService {
    List<MenuResponse> getAllMenus();

    void addMenu(MenuRequest menuRequest);

    MenuResponse getMenuById(Long id);

    MenuResponse getMenuByPopupbarId(Long popupbarId);

    void changeMenu(MenuRequest menuRequest, Long id);

    void deleteMenu(Long id);

    MenuResponse getMenuByCocktailId(Long id);

    void addCocktailToMenu(Long id, Long cocktailId, double price);

    List<CocktailResponse> getOrderCocktailsByPopUpBarId(Long id) throws Exception;

    void addOrderCocktailToPopUpBar(Long idPopUpBar, Long idCocktail) throws Exception;

    void removeCocktailFromMenu(Long id, Long cocktailId);

    void changeCocktailPrice(double cocktailPrice, Long cocktailId, Long menuId);
}
