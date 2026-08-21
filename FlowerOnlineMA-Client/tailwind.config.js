/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{html,ts}",
  ],
  theme: {
    extend: {
      colors:{
        // Fondos beige / crema orgánicos
        'cream-bg': '#F8F6F0',
        'surface-card': '#FFFFFF',
        'surface-low': '#F4F4EF',
        
        // Tonos de marca (Verdes)
        'brand-dark': '#002C15',      // Texto principal y Headers oscuros
        'brand-primary': '#174328',   // Botones primarios, Banners y Footer
        'brand-accent': '#2D7A47',    // Botones de acción / CTA
        'brand-light': '#A2F2B3',     // Badges, destacados y chips
        
        // Texto
        'text-dark': '#1A1C19',
        'text-muted': '#414942',
      }
    },
  },
  plugins: [],
}