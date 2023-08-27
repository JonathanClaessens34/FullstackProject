package be.pxl.services.domain.dto;

import be.pxl.services.domain.Category;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class CocktailResponse {

    private long serialNumber;

    private String name;
    private double purchasePrice;
    private Category category;
    private String imageUrl;
}
