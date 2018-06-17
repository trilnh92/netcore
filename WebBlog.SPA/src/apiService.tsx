import { fetchApi, fetchLogin } from './helper';
import { BaseUrl } from './base.url';
import { ServiceUrl } from './service.url';
import { AuthUrl } from './auth.url';

/* Articles */
export function apiGetArticles(callback: any, errorCallback: any) {
    var url = BaseUrl.BASE_URL + ServiceUrl.ARTICLE_URL;
    fetchApi(url, 'GET', undefined, callback, errorCallback);
}

export function apiGetArticleById(articleId: number, callback: any, errorCallback: any) {
    var url = BaseUrl.BASE_URL + ServiceUrl.ARTICLE_URL + articleId;
    fetchApi(url, 'GET', undefined, callback, errorCallback);
}

/* Categories */
export function apiGetCategories(callback: any, errorCallback: any) {
    var url = BaseUrl.BASE_URL + ServiceUrl.CATEGORY_URL;
    fetchApi(url, 'GET', undefined, callback, errorCallback);
}

/* Account */
export function apiRegisterAccount(registerModel:any, callback:any, errorCallback:any){
    var url = BaseUrl.AUTH_URL + AuthUrl.REGISTER_URL;
    fetchApi(url, 'POST', registerModel, callback, errorCallback);
}

export function apiLoginAccount(loginModel:any, callback:any, errorCallback:any){
    var url = BaseUrl.AUTH_URL + '/connect/token';
    fetchLogin(url, loginModel, callback, errorCallback);
}