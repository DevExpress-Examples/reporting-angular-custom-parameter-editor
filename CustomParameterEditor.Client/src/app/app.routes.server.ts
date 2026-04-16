import { RenderMode, ServerRoute } from '@angular/ssr';

export const serverRoutes: ServerRoute[] = [
  { path: 'DocumentViewer', renderMode: RenderMode.Client },
  { path: '', renderMode: RenderMode.Prerender },
  { path: '**', renderMode: RenderMode.Client }
];