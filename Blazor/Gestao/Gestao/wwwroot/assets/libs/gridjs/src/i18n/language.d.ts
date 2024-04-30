type MessageFormat = (...args: any[]) => string;
type Message = string | MessageFormat;
export type Language = {
    [key: string]: Message | Language;
};
export declare class Translator {
    private readonly _language;
    private readonly _defaultLanguage;
    constructor(language?: Language);
    getString(message: string, lang: Language): MessageFormat;
    translate(message: string, ...args: any[]): string;
}
export declare function useTranslator(): (message: string, ...args: any[]) => string;
export {};
