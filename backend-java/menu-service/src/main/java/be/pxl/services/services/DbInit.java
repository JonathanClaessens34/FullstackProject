package be.pxl.services.services;

import be.pxl.services.domain.User;
import be.pxl.services.repository.UserRepository;
import org.springframework.boot.CommandLineRunner;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;

@Service
public class DbInit implements CommandLineRunner {
    private UserRepository userRepository;
    private PasswordEncoder passwordEncoder;

    public DbInit(UserRepository userRepository, PasswordEncoder passwordEncoder) {
        this.userRepository = userRepository;
        this.passwordEncoder = passwordEncoder;
    }

    @Override
    public void run(String... args) throws Exception {
        //Delete all
        this.userRepository.deleteAll();

        //create user
        User brewer = new User(1L,"brewer", passwordEncoder.encode("brewer123"), "ADMIN");

        //save user to db
        this.userRepository.save(brewer);
    }
}
