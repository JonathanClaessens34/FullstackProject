package be.pxl.services.services;

import be.pxl.services.domain.PopUpBar;
import be.pxl.services.domain.dto.PopUpBarRequest;
import be.pxl.services.domain.dto.PopUpBarResponse;

import java.util.List;

public interface IPopUpBarService {

    List<PopUpBarResponse> getAllPopUpBars();

    void addPopUpBar(PopUpBarRequest popUpBarRequest);

    PopUpBarResponse getPopUpBarById(Long id);

    void changePopUpBar(PopUpBarRequest popUpBarRequest, Long id);

    void deletePopUpBar(Long id);
}
