import { fetchApi, fetchImageApi, fetchMultipartApi } from './helper';
import { BaseUrl } from './base.url';
import { ServiceUrl } from './service.url';

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
