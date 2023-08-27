package be.pxl.services.domain.client;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.util.List;

@Data
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class MenuRequest {

    private Long popUpBarId;
    private List<MenuItem> cocktails;
    private List<Cocktail> orderCocktails;
}
