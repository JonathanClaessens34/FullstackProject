package be.pxl.services.client;

import be.pxl.services.domain.client.MenuRequest;
import org.springframework.cloud.openfeign.FeignClient;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;

@FeignClient(name = "menu-service")
public interface MenuClient {

    @PostMapping(value = "/api/createMenu/")
    void addMenu(@RequestBody MenuRequest menuRequest );
}
