package be.pxl.services.messagingservice;

import be.pxl.services.domain.dto.*;

import lombok.RequiredArgsConstructor;
import lombok.SneakyThrows;
import lombok.extern.slf4j.Slf4j;
import org.springframework.amqp.rabbit.core.RabbitTemplate;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;


@RestController
@RequestMapping("/api/messaging")
@Slf4j
@RequiredArgsConstructor
public class MessagingController {

    @Autowired
    private final RabbitTemplate rabbitTemplate;

    @SneakyThrows
    @PostMapping("/addMenu")
    @ResponseStatus(HttpStatus.OK)
    public ResponseEntity sendNewBarMessage(@RequestBody AddMenuRequest addMenuRequest){

        String menuString = String.format("{ \"menuId\" : \"%s\", \"BarName\" : \"%s\" }", addMenuRequest.getMenuId(),addMenuRequest.getBarName());

        System.out.println(menuString);
        System.out.println("Sending message...");
        rabbitTemplate.convertAndSend(MessagingServiceApplication.directExchangeName, "MenuAddedIntegrationEvent", menuString);
        System.out.println("Done");
        return new ResponseEntity<>(HttpStatus.OK);
    }

    @SneakyThrows
    @DeleteMapping("/deleteMenu")
    @ResponseStatus(HttpStatus.OK)
    public ResponseEntity sendMenuDeleteMessage(@RequestBody DeleteMenuRequest deleteMenuRequest){

        String menuString = String.format("{ \"MenuId\" : \"%s\"}", deleteMenuRequest.getMenuId());
        System.out.println(menuString);
        System.out.println("Sending message...");
        rabbitTemplate.convertAndSend(MessagingServiceApplication.directExchangeName, "MenuDeletedIntegrationEvent", menuString);
        System.out.println("Done");
        return new ResponseEntity<>(HttpStatus.OK);
    }

    @SneakyThrows
    @PostMapping("/addCocktail")
    @ResponseStatus(HttpStatus.OK)
    public ResponseEntity sendNewCocktailMessage(@RequestBody AddCocktailRequest addCocktailRequest){

        String cocktailString = String.format("{ \"Name\" : \"%s\",\"SerialNumber\" : \"%s\",\"ImageUrl\" : \"%s\" }", addCocktailRequest.getName(),addCocktailRequest.getGtin13nr(),addCocktailRequest.getImage());

        System.out.println(cocktailString);

        System.out.println("Sending message...");
        rabbitTemplate.convertAndSend(MessagingServiceApplication.directExchangeName, "CocktailAddedIntegrationEvent", cocktailString);
        System.out.println("Done");
        return new ResponseEntity<>(HttpStatus.OK);
    }


    @SneakyThrows
    @PutMapping("/changeCocktail")
    @ResponseStatus(HttpStatus.OK)
    public ResponseEntity sendCocktailChangeMessage(@RequestBody AddCocktailRequest addCocktailRequest){

        String cocktailString = String.format("{ \"Name\" : \"%s\",\"SerialNumber\" : \"%s\",\"ImageUrl\" : \"%s\" }", addCocktailRequest.getName(),addCocktailRequest.getGtin13nr(),addCocktailRequest.getImage());
        System.out.println(cocktailString);
        System.out.println("Sending message...");
        rabbitTemplate.convertAndSend(MessagingServiceApplication.directExchangeName, "CocktailAddedIntegrationEvent", cocktailString);
        System.out.println("Done");
        return new ResponseEntity<>(HttpStatus.OK);
    }

    @SneakyThrows
    @DeleteMapping("/deleteCocktail")
    @ResponseStatus(HttpStatus.OK)
    public ResponseEntity sendCocktailDeleteMessage(@RequestBody DeleteCocktailRequest deleteCocktailRequest){

        String cocktailString = String.format("{ \"SerialNumber\" : \"%s\"}", deleteCocktailRequest.getSerialNumber());
        System.out.println(cocktailString);
        System.out.println("Sending message...");
        rabbitTemplate.convertAndSend(MessagingServiceApplication.directExchangeName, "CocktailDeletedIntegrationEvent", cocktailString);
        System.out.println("Done");
        return new ResponseEntity<>(HttpStatus.OK);
    }

    @SneakyThrows
    @PostMapping("/addCocktailToMenu")
    @ResponseStatus(HttpStatus.OK)
    public ResponseEntity sendNewMenuMessage(@RequestBody AddCocktailToMenuRequest addCocktailToMenuRequest){

        String cocktailString = String.format("{ \"menuId\" : \"%s\",\"SerialNumber\" : \"%s\",\"Price\" : \"%s\" }", addCocktailToMenuRequest.getMenuId(),addCocktailToMenuRequest.getCocktailId(),addCocktailToMenuRequest.getPrice());

        System.out.println(cocktailString);

        System.out.println("Sending message...");
        rabbitTemplate.convertAndSend(MessagingServiceApplication.directExchangeName, "CocktailAddedToMenuIntegrationEvent", cocktailString);
        System.out.println("Done");
        return new ResponseEntity<>(HttpStatus.OK);
    }

    @SneakyThrows
    @DeleteMapping("/deleteCocktailFromMenu")
    @ResponseStatus(HttpStatus.OK)
    public ResponseEntity sendCocktailDeleteMessage(@RequestBody DeleteCocktailFromMenu deleteCocktailFromMenu){

        String cocktailString = String.format("{ \"menuId\" : \"%s\",\"SerialNumber\" : \"%s\"}", deleteCocktailFromMenu.getMenuId(),deleteCocktailFromMenu.getSerialNumber());
        System.out.println(cocktailString);
        System.out.println("Sending message...");
        rabbitTemplate.convertAndSend(MessagingServiceApplication.directExchangeName, "CocktailDeletedFromMenuIntegrationEvent", cocktailString);
        System.out.println("Done");
        return new ResponseEntity<>(HttpStatus.OK);
    }

}
