package be.pxl.services.controller;

import be.pxl.services.domain.dto.PopUpBarRequest;
import be.pxl.services.domain.dto.PopUpBarResponse;
import be.pxl.services.services.IPopUpBarService;
import lombok.RequiredArgsConstructor;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import javax.ws.rs.GET;

@RestController
@RequestMapping("/api")
@RequiredArgsConstructor
public class PopUpBarController {
    private final IPopUpBarService popUpBarService;

    @GetMapping("/allpopupbars")
    public ResponseEntity getPopUpBar(){
        return new ResponseEntity(popUpBarService.getAllPopUpBars(), HttpStatus.OK);
    }

    @PostMapping("/createpopupbar")
    @ResponseStatus(HttpStatus.CREATED)
    public void addPopUpBar(@RequestBody PopUpBarRequest popUpBarRequest){
        popUpBarService.addPopUpBar(popUpBarRequest);
    }

    @GetMapping("/popupbar/{id}")
    public ResponseEntity<PopUpBarResponse> getPopUpBarById(@PathVariable Long id){
        return new ResponseEntity(popUpBarService.getPopUpBarById(id), HttpStatus.OK);
    }

    @PutMapping("/popupbar/{id}")
    @ResponseStatus(HttpStatus.OK)
    public void changePopUpBar(@RequestBody PopUpBarRequest popUpBarRequest, @PathVariable Long id){
        popUpBarService.changePopUpBar(popUpBarRequest, id);
    }

    @DeleteMapping("/popupbar/{id}")
    @ResponseStatus(HttpStatus.OK)
    public void deletePopUpBar(@PathVariable Long id){
        popUpBarService.deletePopUpBar(id);
    }
}
