import { AlertMessages, ApiUrls } from "../utilHelpers/Constants";
import ApiResponse from "../models/ApiResponse"
import axios from 'axios';

export class ChatRoomApiService {

  /**  Method to get all the chat rooms */
  public async getChatRooms(): Promise<ApiResponse | any> {
    try {
      const apiResponse: ApiResponse = { IsSuccess: false, Message: '' };
      const apiURL = ApiUrls.chatRoomApiURL + ApiUrls.listURLPath;

      await axios.get(apiURL)
        .then((response) => {
          // if response is success, return the ChatRoomsList
          if (response.data != null) {
            apiResponse.IsSuccess = true;
            apiResponse.Data = response.data.data;
          }
        })
        .catch((ex) => {
          var result = ex.message;
          console.log(result);
          apiResponse.Message = AlertMessages.APIOnline;
        });

      return apiResponse;
    }
    catch (error) {
      console.error(error);
      return null;
    }
  }
}