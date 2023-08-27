package be.pxl.services.domain;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.*;

@Entity
@Table(name = "cocktail")
@Data
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class Cocktail {

    @Id
    private Long serialNumber;

    private String name;
    private double purchasePrice;
    private Category category;
    private String imageUrl;

}
