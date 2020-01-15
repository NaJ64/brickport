import { ICommand } from "./command";

export interface ICommandHandler<TCommand extends ICommand, TResult = "string"> { 
    handleAsync(command: TCommand): Promise<TResult>;
}