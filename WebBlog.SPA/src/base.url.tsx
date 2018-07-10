import { environment } from './environments/environment';

export class BaseUrl {
    public static get BASE_URL(): string { return environment.serviceAPI; }
    public static get AUTH_URL(): string { return environment.authAPI; }
    public static get HOME_URL(): string {return environment.clientURL + '/';}
    public static get ARTICLE_DETAIL_URL(): string {return environment.clientURL + '/article/:articleId';}
    public static get LOGIN_URL(): string {return environment.clientURL + '/login';}
    public static get REGISTER_URL(): string {return environment.clientURL + '/register';}
    public static get MYPROFILE_URL(): string {return environment.clientURL + '/myprofile';}
    public static get MYBLOGS_URL(): string {return environment.clientURL + '/myblogs';}
    public static get CATEGORYBLOGS_URL(): string {return environment.clientURL + '/categoryblogs';}
    public static get CATEGORYBLOGS_DETAIL_URL(): string {return environment.clientURL + '/categoryblogs/:category';}
    public static get CREATE_BLOG_URL(): string {return environment.clientURL + '/createblog';}    
    public static get ARTICLE_URL():string {return environment.clientURL +'/article'}
}