import axios, { AxiosResponse, AxiosError } from "axios";

interface BrickportApiLogEntry { timestamp: number, url: string, method: string, state: string, message: string };

export class BrickportApiClient {

    private readonly _endpoint: string;
    private readonly _enableLogging: boolean;
    private readonly _log: BrickportApiLogEntry[];
    
    constructor(endpoint: string, enableLogging?: boolean) {
        this._endpoint = endpoint;
        this._enableLogging = !!enableLogging;
        this._log = [];
    }

    private logMessage(url: string, method: string, state: 'request' | 'response' | 'error', message?: string) {
        if (!this._enableLogging) {
            return;
        }
        const entry = { timestamp: performance.now(), url, method, state, message: message || "" };
        this._log.push(entry);
        if (entry.message) {
            console.log(entry.timestamp, entry.method, entry.url, entry.state.toLocaleUpperCase(), entry.message);
        } else {
            console.log(entry.timestamp, entry.method, entry.url, entry.state.toLocaleUpperCase());
        }
    }

    logEntryToText(entry: BrickportApiLogEntry): string {
        return `${entry.method} ${entry.url} | ${entry.state} | ${entry.message}`
    }

    log(): [number, string][] {
        return this._log.map(x => [x.timestamp, this.logEntryToText(x)]);
    }

    private fixAddress(address?: string): string {
        if (!address) {
            address = "";
        }
        if (address.length && address.substr(0,1) !== "/") {
            address = "/" + address;
        }
        return address;
    }

    async get<TResponse>(address?: string): Promise<TResponse> {
        address = this.fixAddress(address);
        const url = `${this._endpoint}${address}`;
        this.logMessage(url, 'GET', 'request')
        try {
            const response = await axios.get<TResponse>(url);
            this.logMessage(url, 'GET', 'response', JSON.stringify(response.data));
            return response.data;
        } catch (error) {
            this.logMessage(url, 'GET', 'error', (<AxiosError>error).message);
            throw error;
        }
    }

    async post<TResponse = any>(data?: any, address?: string): Promise<TResponse> {
        address = this.fixAddress(address);
        const url = `${this._endpoint}${address}`;
        this.logMessage(url, 'POST', 'request', JSON.stringify(data));
        try {
            const response = await axios.post<TResponse>(url, data || undefined);
            this.logMessage(url, 'POST', 'response', JSON.stringify(response.data));
            return response.data;
        } catch (error) {
            this.logMessage(url, 'POST', 'error', (<AxiosError>error).message);
            throw error;
        }

    }

}