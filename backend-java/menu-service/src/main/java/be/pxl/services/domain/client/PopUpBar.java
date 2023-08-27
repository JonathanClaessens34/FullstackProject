package be.pxl.services.domain.client;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.*;

@Data
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class PopUpBar {

    private Long id;

    private String name;
    private String location;
    private String brewer;


}
