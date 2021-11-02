export interface AppConfig {
  style: {
    breakpoints: {
      [key: string]: number;
    };
  };
  componentPaths: {
    [key: string]: string[];
  };
}
