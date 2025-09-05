import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  public forecasts: WeatherForecast[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.getForecasts();
  }

   getApiUri() {
  //let baseUri = window.location.protocol + "//";
  //baseUri += window.location.host;
  //baseUri += window.location.pathname;
  //const locationRoute = AppConfiguration.routeUrl.location.replace("/", "");
  //return baseUri.replace(`${locationRoute}`, '');

  const origin = window.location.origin;
  const baseElement = document.querySelector('base');
  const baseHref = baseElement ? baseElement.getAttribute('href') : null;
  const baseUri = `${origin}${baseHref}api/weather`;

  if(baseUri.includes('http://127.0.0.1:4200')) {
    return 'http://localhost:5022/api/weather';
  }

  return baseUri;
}

  getForecasts() {
    this.http.get<WeatherForecast[]>(this.getApiUri()).subscribe(
      (result) => {
        this.forecasts = result;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  title = 'dataplatform.client';
}
