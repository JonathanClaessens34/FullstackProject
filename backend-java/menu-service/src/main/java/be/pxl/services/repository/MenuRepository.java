package be.pxl.services.repository;

import be.pxl.services.domain.Menu;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;


@Repository
public interface MenuRepository extends JpaRepository<Menu, Long> {
    Menu findMenuByPopUpBarId(Long popupbarId);

}
