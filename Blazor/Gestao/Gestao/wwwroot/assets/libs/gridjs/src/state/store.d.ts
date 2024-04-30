export declare class Store<S = Record<string, unknown>> {
    private state;
    private listeners;
    private isDispatching;
    constructor(initialState: S);
    getState: () => S;
    getListeners: () => ((current?: S, prev?: S) => void)[];
    dispatch: (reducer: (state: S) => S) => S;
    subscribe: (listener: (current?: S, prev?: S) => void) => (() => void);
}
