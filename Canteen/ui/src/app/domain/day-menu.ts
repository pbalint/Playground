import { Dish } from './dish';

export class DayMenu {
    date: string;
    dayNumber: number;
    dishes: Map<string, Dish[]>;
}
