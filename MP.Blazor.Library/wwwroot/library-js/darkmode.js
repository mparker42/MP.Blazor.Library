export function prefersDarkMode() {
    return window.matchMedia('(prefers-color-scheme: dark)').matches;
}