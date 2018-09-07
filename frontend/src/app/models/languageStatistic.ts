export interface LanguageStatistic {
    
    id: number;
    
    name: string;
    
    code: string;

    translatedStringsCount: number;

    complexStringsCount: number;

    proficiency: Proficiency;
}

export enum Proficiency{
    Beginner,
    Elementary,
    Intermediate,
    UpperIntermediate,
    Advanced,
    Proficiency
}