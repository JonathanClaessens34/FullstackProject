package be.pxl.services.client;

import be.pxl.services.domain.client.PopUpBarRequest;
import be.pxl.services.domain.client.PopUpBarResponse;
import org.springframework.cloud.openfeign.FeignClient;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@FeignClient(name = "popupbar-service")
public interface PopUpBarClient {

    @PostMapping("/createPopUpBar")
    @ResponseStatus(HttpStatus.CREATED)
    void addPopUpBar(@RequestBody PopUpBarRequest popUpBarRequest);

    @GetMapping("/api/popupbar/{id}/")
    ResponseEntity<PopUpBarResponse> getPopUpBarById(@PathVariable Long id);
}
