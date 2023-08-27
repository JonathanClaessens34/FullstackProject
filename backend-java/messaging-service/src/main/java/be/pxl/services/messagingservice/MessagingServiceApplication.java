package be.pxl.services.messagingservice;

import org.springframework.amqp.core.*;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.Bean;

@SpringBootApplication
public class MessagingServiceApplication {

    static final String directExchangeName = "ordermgmt_event_bus";

    /**
     * We define our queue info
     *
     * @return Queue object
     */
    @Bean
    public Queue myQueue() {
        return new Queue("myQueue", false);
    }

    @Bean
    DirectExchange exchange() {
        return new DirectExchange(directExchangeName);
    }

    @Bean
    Binding binding(Queue queue, DirectExchange exchange) {
        return BindingBuilder.bind(queue).to(exchange).with("CocktailAddedIntegrationEvent"); //dees is lezen van de que denk
    }


    public static void main(String[] args) {
        SpringApplication.run(MessagingServiceApplication.class, args);
    }



}
