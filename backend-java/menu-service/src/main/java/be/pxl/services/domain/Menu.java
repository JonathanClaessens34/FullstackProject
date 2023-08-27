package be.pxl.services.domain;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.*;
import java.util.ArrayList;
import java.util.List;
import java.util.UUID;

@Entity
@Table(name = "menu")
@Data
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class Menu {

    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private Long id;

    private UUID uuid;
    private Long popUpBarId;
    @OneToMany(cascade = CascadeType.ALL)
    private List<MenuItem> cocktails = new ArrayList<MenuItem>();
    @ManyToMany
    private List<Cocktail> orderCocktails = new ArrayList<Cocktail>();
}
