import { fetchApi, fetchImageApi, fetchMultipartApi } from './helper';
import { BaseUrl } from './base.url';
import { ServiceUrl } from './service.url';

/* Articles */
export function apiGetArticles(callback:any, errorCallback:any) {
    var url = BaseUrl.BASE_URL+ServiceUrl.ARTICLE_URL;
    fetchApi(url, 'GET', undefined, callback, errorCallback);
}

