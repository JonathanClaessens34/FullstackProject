package be.pxl.services.domain.client;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class PopUpBarRequest {

    private String name;
    private String location;
    private String brewer;

}
