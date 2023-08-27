package be.pxl.services.services;

import be.pxl.services.domain.Cocktail;
import be.pxl.services.domain.SerialNumber;
import be.pxl.services.domain.dto.CocktailRequest;
import be.pxl.services.domain.dto.CocktailResponse;
import be.pxl.services.repository.CocktailRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;
import org.springframework.web.client.RestTemplate;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

@Service
@RequiredArgsConstructor
public class CocktailService implements ICocktailService{

    private final CocktailRepository cocktailRepository;
    @Override
    public List<CocktailResponse> getAllCocktails() {
        List<Cocktail> cocktails = cocktailRepository.findAll();
        return cocktails.stream().map(cocktail -> mapToCocktailResponse(cocktail)).toList();
    }

    public CocktailResponse mapToCocktailResponse(Cocktail cocktail) {
        return CocktailResponse.builder()
                .serialNumber(cocktail.getSerialNumber())
                .name(cocktail.getName())
                .purchasePrice(cocktail.getPurchasePrice())
                .category(cocktail.getCategory())
                .imageUrl(cocktail.getImageUrl())
                .build();
    }

    @Override
    public void addCocktail(CocktailRequest cocktailRequest) {
        String id = String.valueOf(cocktailRequest.getSerialNumber());
        try {
            SerialNumber.checkSerialNumber(id);
            Cocktail cocktail = Cocktail.builder()
                    .serialNumber(cocktailRequest.getSerialNumber())
                    .name(cocktailRequest.getName())
                    .purchasePrice(cocktailRequest.getPurchasePrice())
                    .category(cocktailRequest.getCategory())
                    .imageUrl(cocktailRequest.getImageUrl())
                    .build();
            Cocktail newCocktail = cocktailRepository.save(cocktail);

            //send to eventbus

            // create a map containing the request body data
            Map<String, Object> requestBody = new HashMap<>();
            requestBody.put("name", newCocktail.getName());
            requestBody.put("gtin13nr", newCocktail.getSerialNumber());
            requestBody.put("image", newCocktail.getImageUrl());

            // create a RestTemplate instance
            RestTemplate restTemplate = new RestTemplate();

            // make a POST request to the specified URL, sending the request body data
            restTemplate.postForObject("http://localhost:8086/api/messaging/addCocktail", requestBody, String.class);
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    @Override
    public CocktailResponse getCocktailById(Long id) {
        Cocktail cocktail = cocktailRepository.findById(id).get();
        return mapToCocktailResponse(cocktail);
    }

    @Override
    public void changeCocktail(CocktailRequest cocktailRequest, Long id) {
        cocktailRepository.findById(id)
                .map(cocktail -> {
                    cocktail.setSerialNumber(cocktailRequest.getSerialNumber());
                    cocktail.setName(cocktailRequest.getName());
                    cocktail.setPurchasePrice(cocktailRequest.getPurchasePrice());
                    cocktail.setCategory(cocktailRequest.getCategory());
                    cocktail.setImageUrl(cocktailRequest.getImageUrl());
                    return cocktailRepository.save(cocktail);
                });
        Cocktail newCocktail = cocktailRepository.findById(id).get();
        //send to eventbus

        // create a map containing the request body data
        Map<String, Object> requestBody = new HashMap<>();
        requestBody.put("name", newCocktail.getName());
        requestBody.put("gtin13nr", newCocktail.getSerialNumber());
        requestBody.put("image", newCocktail.getImageUrl());

        // create a RestTemplate instance
        RestTemplate restTemplate = new RestTemplate();

        // make a POST request to the specified URL, sending the request body data
        restTemplate.postForObject("http://localhost:8086/api/messaging/addCocktail", requestBody, String.class);
    }

    @Override
    public void deleteCocktail(Long id) {
        cocktailRepository.deleteById(id);

        //send to eventbus

        // create a map containing the request body data
        Map<String, Object> requestBody = new HashMap<>();
        requestBody.put("serialNumber", id);


        // create a RestTemplate instance
        RestTemplate restTemplate = new RestTemplate();

        // make a POST request to the specified URL, sending the request body data
        restTemplate.postForObject("http://localhost:8086/api/messaging/deleteCocktail", requestBody, String.class);
    }


}
