package be.pxl.services.services;

import be.pxl.services.domain.Cocktail;
import be.pxl.services.domain.dto.CocktailRequest;
import be.pxl.services.domain.dto.CocktailResponse;

import java.util.List;

public interface ICocktailService {

    List<CocktailResponse> getAllCocktails();

    void addCocktail(CocktailRequest cocktailRequest);

    CocktailResponse getCocktailById(Long id);

    void changeCocktail(CocktailRequest cocktailRequest, Long id);

    void deleteCocktail(Long id);

}
