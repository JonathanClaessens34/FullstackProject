package be.pxl.services.repository;

import be.pxl.services.domain.Menu;
import be.pxl.services.domain.User;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface UserRepository extends JpaRepository<User, Long> {
    User findByUsername(String username);
}
