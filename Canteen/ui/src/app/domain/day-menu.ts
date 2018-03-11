import { Dish } from './dish';

export class DayMenu {
    date: string;
    dishes: Map<String, Dish[]>;
}
