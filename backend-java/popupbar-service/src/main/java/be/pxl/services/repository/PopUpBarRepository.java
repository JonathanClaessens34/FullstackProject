package be.pxl.services.repository;

import be.pxl.services.domain.PopUpBar;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface PopUpBarRepository extends JpaRepository<PopUpBar, Long> {

}

