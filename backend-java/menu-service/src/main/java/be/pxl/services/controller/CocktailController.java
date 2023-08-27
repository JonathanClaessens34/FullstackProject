package be.pxl.services.controller;

import be.pxl.services.domain.dto.CocktailRequest;

import be.pxl.services.services.ICocktailService;
import lombok.RequiredArgsConstructor;
import org.json.simple.JSONObject;
import org.springframework.cloud.context.config.annotation.RefreshScope;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;


@RestController
@RequestMapping("/api")
@RequiredArgsConstructor
public class CocktailController {

    private final ICocktailService cocktailService;

    @GetMapping("/allCocktails")
    public ResponseEntity getCocktails(){
        return new ResponseEntity(cocktailService.getAllCocktails(), HttpStatus.OK);
    }

    @PostMapping("/createCocktail")
    @ResponseStatus(HttpStatus.CREATED)
    public void addCocktail(@RequestBody CocktailRequest cocktailRequest){
        cocktailService.addCocktail(cocktailRequest);
    }

    @GetMapping("/cocktail/{id}")
    public ResponseEntity getCocktailById(@PathVariable Long id){
        return new ResponseEntity(cocktailService.getCocktailById(id), HttpStatus.OK);
    }

    @PutMapping("/cocktail/{id}")
    @ResponseStatus(HttpStatus.OK)
    public void changeCocktail(@RequestBody CocktailRequest cocktailRequest, @PathVariable Long id){
        cocktailService.changeCocktail(cocktailRequest, id);
    }

    @DeleteMapping("/cocktail/{id}")
    @ResponseStatus(HttpStatus.OK)
    public void deleteCocktail(@PathVariable Long id){
        cocktailService.deleteCocktail(id);
    }

}
