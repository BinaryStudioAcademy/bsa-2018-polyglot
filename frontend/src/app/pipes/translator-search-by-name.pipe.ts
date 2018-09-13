import { Pipe, PipeTransform } from '@angular/core';
import { Translator } from '../models/Translator';

@Pipe({
    name: 'translatorSearchByName'
})
export class TranslatorSearchByNamePipe implements PipeTransform {

    transform(items: Translator[], searchQuery: string): any[] {
        if (!items) return [];
        if (!searchQuery) return items;
        searchQuery = searchQuery.toLowerCase();
        return items.filter(it => {
            return it.fullName.toLowerCase().includes(searchQuery);
        });
    }

}
