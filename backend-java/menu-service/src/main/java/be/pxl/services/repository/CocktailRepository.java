package be.pxl.services.repository;

import be.pxl.services.domain.Cocktail;
import be.pxl.services.domain.Menu;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface CocktailRepository extends JpaRepository<Cocktail, Long> {
}
