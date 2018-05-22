import { environment } from './environments/environment';

export class BaseUrl {
    public static get BASE_URL(): string { return environment.serviceAPI; }
    public static get AUTH_URL(): string { return environment.authAPI; }
}