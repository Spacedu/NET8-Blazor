import { Config } from '../config';
import Storage from './storage';
declare class StorageUtils {
    static createFromConfig(config: Config): Storage<any>;
    static tableElementToArray(element: HTMLElement): any[][];
}
export default StorageUtils;
