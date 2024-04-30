import { Config } from './config';
import { VNode } from 'preact';
import { EventEmitter } from './util/eventEmitter';
import { GridEvents } from './events';
import { PluginManager } from './plugin';
declare class Grid extends EventEmitter<GridEvents> {
    config: Config;
    plugin: PluginManager;
    constructor(config?: Partial<Config>);
    updateConfig(config: Partial<Config>): this;
    createElement(): VNode;
    forceRender(): this;
    destroy(): void;
    render(container: Element): this;
}
export default Grid;
