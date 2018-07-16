import { fetchApi, fetchLogin, fetchImageApi } from './helper';
import { BaseUrl } from './base.url';
import { ServiceUrl } from './service.url';
import { AuthUrl } from './auth.url';

/* Articles */
export function apiGetArticles(callback: any, errorCallback: any) {
    var url = BaseUrl.BASE_URL + ServiceUrl.ARTICLE_URL;
    fetchApi(url, 'GET', undefined, callback, errorCallback);
}

export function apiGetArticlesByUser(userModel:any, callback:any, errorCallback:any){
    var url = BaseUrl.BASE_URL + ServiceUrl.ARTICLE_URL + 'GetArticlesByUser';
    fetchApi(url, 'POST', userModel, callback, errorCallback);
}

export function apiGetArticlesByCategory(category:string, callback:any, errorCallback:any){
    var url = BaseUrl.BASE_URL + ServiceUrl.ARTICLE_URL + 'GetArticlesByCategory/' + category;
    fetchApi(url, 'GET', undefined, callback, errorCallback);
}

export function apiGetArticlesByUserPaging(page:number, userModel:any, callback:any, errorCallback:any){
    var url = BaseUrl.BASE_URL + ServiceUrl.ARTICLE_URL + 'GetArticlesByUser/' + page;
    fetchApi(url, 'POST', userModel, callback, errorCallback);
}

export function apiGetArticlesByCategoryPaging(category:string, page:number, callback:any, errorCallback:any){
    var url = BaseUrl.BASE_URL + ServiceUrl.ARTICLE_URL + 'GetArticlesByCategory/' + category + '/' + page;
    fetchApi(url, 'GET', undefined, callback, errorCallback);
}

export function apiSearchArticlesPaging(search:string, page:number, callback:any, errorCallback:any){
    var url = BaseUrl.BASE_URL + ServiceUrl.ARTICLE_URL + 'SearchArticles/' + search + '/' + page;
    fetchApi(url, 'GET', undefined, callback, errorCallback);
}

export function apiGetArticlesPaging(page:number, callback:any, errorCallback:any){
    var url = BaseUrl.BASE_URL + ServiceUrl.ARTICLE_URL + 'GetArticlesPaging/' + page;
    fetchApi(url, 'GET', undefined, callback, errorCallback);
}

export function apiGetArticleById(articleId: number, callback: any, errorCallback: any) {
    var url = BaseUrl.BASE_URL + ServiceUrl.ARTICLE_URL + articleId;
    fetchApi(url, 'GET', undefined, callback, errorCallback);
}

export function apiCreateArticle(article: any, callback: any, errorCallback: any) {
    var url = BaseUrl.BASE_URL + ServiceUrl.ARTICLE_URL;
    fetchApi(url, 'POST', article, callback, errorCallback);
}

export function apiUploadPhoto(articleImage:any, callback:any, errorCallback:any) {
    var url = BaseUrl.BASE_URL + ServiceUrl.ARTICLE_URL + 'UploadPhoto';
    fetchImageApi(url, 'POST', articleImage, callback, errorCallback);
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

/* Comment */
export function apiGetCommentsByArticleById(articleId:number, callback:any, errorCallback:any){
    var url = BaseUrl.BASE_URL + ServiceUrl.COMMENT_URL + 'GetCommentsByArticleById/' + articleId;
    fetchApi(url, 'GET', undefined, callback, errorCallback);
}

export function apiCreateComment(comment:any, callback:any, errorCallback:any){
    var url = BaseUrl.BASE_URL + ServiceUrl.COMMENT_URL;
    fetchApi(url, 'POST', comment , callback, errorCallback);
}