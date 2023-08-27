package be.pxl.services.domain;


public class SerialNumber {

    public static void checkSerialNumber(String gtin13) throws Exception {
        if(gtin13.length() != 13){
            throw new Exception("GTIN13 nummer klopt niet");
        }
        int control = 0;
        for (int i = 0; i < 13; i++) {
            int digit = Integer.parseInt(gtin13.substring(i, i + 1));
            if (i % 2 == 0) {
                control += digit * 3;
            } else {
                control += digit;
            }
        }
        int check = 10 - (control % 10);
        if(check == 10){
            check = 0;
        }
    }
}
