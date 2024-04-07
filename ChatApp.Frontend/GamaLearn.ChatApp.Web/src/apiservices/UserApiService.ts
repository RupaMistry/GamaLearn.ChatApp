import { AlertMessages, ApiUrls } from "../utilHelpers/Constants";
import ApiResponse from "../models/ApiResponse"
import axios from 'axios';
import RegisterUser from "../models/RegisterUser";
import LoginUser from "../models/LoginUser";

export class UserApiService {

  /**  Method to register a new user in system */
  public async registerUser(userName: string, password: string, emailID: string): Promise<ApiResponse | any> {
    try {
      const apiResponse: ApiResponse = { IsSuccess: false, Message: '' };
      const registerURL = ApiUrls.userApiURL + ApiUrls.registerURLPath;

      const registerUser: RegisterUser = {
        UserName: userName,
        Email: emailID,
        Password: password
      };

      await axios.post(registerURL, registerUser)
        .then((response) => {
          if (response.data != null) {
            apiResponse.IsSuccess = true;
            apiResponse.Message = response.data.message;
          }
        })
        .catch((ex) => {
          var result = ex.response.data;
          if (result != null && result.message != null) {
            apiResponse.Message = result.message;
          }
        });

      return apiResponse;
    }
    catch (error) {
      console.error(error);
      return null;
    }
  }

  /**  Method to validate and authenticates user login credentials. */
  public async loginUser(loginUser: LoginUser): Promise<ApiResponse | any> {
    try {
      const apiResponse: ApiResponse = { IsSuccess: false, Message: '' };
      const loginURL = ApiUrls.userApiURL + ApiUrls.loginURLPath;

      await axios.post(loginURL, loginUser)
        .then((response) => {
          if (response.data != null) {
            apiResponse.IsSuccess = true;
            apiResponse.Message = response.data.token;
          }
        })
        .catch((ex) => {
          var result = ex.response.data;
          if (result != null && result.message != null) {
            apiResponse.Message = result.message;
          }
        });

      return apiResponse;
    }
    catch (error) {
      console.error(error);
      return null;
    }
  }

  /**  Method to get all users in system */
  public async getUsers(): Promise<ApiResponse | any> {
    try {
      const apiResponse: ApiResponse = { IsSuccess: false, Message: '' };
      const userListURL = ApiUrls.userApiURL + ApiUrls.listURLPath;

      await axios.get(userListURL)
        .then((response) => {

          if (response.data != null) {
            apiResponse.IsSuccess = true;
            apiResponse.Data = response.data.data;
          }
        })
        .catch((ex) => {
          var result = ex.response;
          console.log(result);
          apiResponse.Message = AlertMessages.SystemError;
        });

      return apiResponse;
    }
    catch (error) {
      console.error(error);
      return null;
    }
  }
}