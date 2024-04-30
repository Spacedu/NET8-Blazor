import { FunctionComponent } from 'preact';
export declare enum PluginPosition {
    Header = 0,
    Footer = 1,
    Cell = 2
}
export interface Plugin<T extends FunctionComponent> {
    id: string;
    position: PluginPosition;
    component: T;
    order?: number;
}
export declare class PluginManager {
    private readonly plugins;
    constructor();
    get<T extends FunctionComponent>(id: string): Plugin<T> | undefined;
    add<T extends FunctionComponent<any>>(plugin: Plugin<T>): this;
    remove(id: string): this;
    list<T extends FunctionComponent>(position?: PluginPosition): Plugin<T>[];
}
export declare function PluginRenderer(props: {
    props?: any;
    pluginId?: string;
    position?: PluginPosition;
}): import("preact").VNode<any>;
