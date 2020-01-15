import { ICommand } from "../command";
import { ICommandHandler } from "../command-handler";

export interface IRunTestCommand extends ICommand {
    dateUtc: string;
} 

export class RunTestCommand implements ICommand { 
    dateUtc!: string;
    constructor(state?: IRunTestCommand) {
        if (state) {
            Object.assign(this, state);
        }
        this.dateUtc = this.dateUtc || new Date().toISOString();
    }
}

export interface IRunTestHandler extends ICommandHandler<RunTestCommand> { }