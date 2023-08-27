package be.pxl.services.repository;

import be.pxl.services.domain.User;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.jdbc.AutoConfigureTestDatabase;
import org.springframework.boot.test.autoconfigure.orm.jpa.DataJpaTest;
import org.springframework.test.context.junit4.SpringRunner;

import static org.junit.jupiter.api.Assertions.*;

@RunWith(SpringRunner.class)
@DataJpaTest
@AutoConfigureTestDatabase(replace = AutoConfigureTestDatabase.Replace.NONE)
public class UserRepositoryTest {
    @Autowired
    private UserRepository userRepository;

    @Test
    public void testFindByUsername() {
        userRepository.deleteAll();
        User user = new User();
        user.setUsername("john");
        user.setPassword("password");
        userRepository.save(user);

        User foundUser = userRepository.findByUsername("john");
        assertEquals("john", foundUser.getUsername());
    }

    @Test
    public void testSaveUser() {
        userRepository.deleteAll();
        User user = new User();
        user.setUsername("jane");
        user.setPassword("password");

        User savedUser = userRepository.save(user);
        assertNotNull(savedUser.getId());
        assertEquals("jane", savedUser.getUsername());
        assertEquals("password", savedUser.getPassword());
    }

    @Test
    public void testDeleteUser() {
        userRepository.deleteAll();
        User user = new User();
        user.setUsername("jake");
        user.setPassword("password");
        userRepository.save(user);

        userRepository.delete(user);
        assertNull(userRepository.findByUsername("jake"));
    }
}