package be.pxl.services.domain.client;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.Table;

@Entity
@Table(name = "cocktail")
@Data
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class Cocktail {

    @Id
    private long serialNumber;

    private String name;
    private double purchasePrice;
    private Category category;
    private String imageUrl;

}
